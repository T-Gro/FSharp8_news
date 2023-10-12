module ArrayParallelBenchmarkCode

open System
open System.Linq
open System.Collections.Generic
open System.Collections.Concurrent
open System.Threading.Tasks
open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Running
open BenchmarkDotNet.Configs
open BenchmarkDotNet.Reports
open BenchmarkDotNet.Columns
open BenchmarkDotNet.Diagnosers
open BenchmarkDotNet.Order
open BenchmarkDotNet.Mathematics

type MyConfig() as this =
    inherit ManualConfig()
    do this.SummaryStyle <- SummaryStyle.Default.WithRatioStyle(RatioStyle.Percentage)
    do this.SummaryStyle <- this.SummaryStyle.WithSizeUnit(SizeUnit.MB)

[<Struct>]
type SampleRecord = {Age : int; Balance : int64; Molecules : float; IsMiddle : bool}
let complexLogic (sr:SampleRecord) = 
    let mutable total = float sr.Balance
    total <- total + sin sr.Molecules
    total <- atan total
    for a=0 to sr.Age do
        total <- total + cos (float a)
    total <- total + float (hash sr)
    total

[<GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)>]
[<MemoryDiagnoser>]
//[<DryJob>]
[<CategoriesColumn>]
[<Config(typeof<MyConfig>)>]
[<Orderer(SummaryOrderPolicy.Declared, MethodOrderPolicy.Declared)>]
type ArrayProcessingBenchmark() =

    member val ArrayWithItems = Unchecked.defaultof<SampleRecord[]> with get,set
    [<Params(500_000, Priority = 0)>] 
    member val NumberOfItems = -1 with get,set

    [<GlobalSetup>]
    member this.GlobalSetup () = 
        let r = new Random(42)
        this.ArrayWithItems <- Array.init this.NumberOfItems (fun i -> 
            { Age = r.Next(18,60)          
              IsMiddle = if i=(this.NumberOfItems/2) then true else false
              Balance = r.NextInt64()
              Molecules = r.NextDouble()})

    // ---------- MinBy --------

    [<BenchmarkCategory("MinBy(calculationFunction)");Benchmark(Baseline=true)>]
    member this.ArrayMinBy() = this.ArrayWithItems |> Array.minBy complexLogic
    
    [<BenchmarkCategory("MinBy(calculationFunction)");Benchmark>]
    member this.PlinqMinBy() = this.ArrayWithItems.AsParallel().MinBy(fun x -> complexLogic x)
        
    [<BenchmarkCategory("MinBy(calculationFunction)");Benchmark>]
    member this.ArrayParallelMinBy() = this.ArrayWithItems |> Array.Parallel.minBy complexLogic

    // ---------- Sum --------

    [<BenchmarkCategory("SumBy(plain field access)");Benchmark(Baseline=true)>]
    member this.ArraySumBy() = this.ArrayWithItems |> Array.sumBy _.Age
    
    [<BenchmarkCategory("SumBy(plain field access)");Benchmark>]
    member this.PlinqSumBy() = this.ArrayWithItems.AsParallel().Sum(fun x -> x.Age)
        
    [<BenchmarkCategory("SumBy(plain field access)");Benchmark>]
    member this.ArrayParallelSumBy() = this.ArrayWithItems |> Array.Parallel.sumBy _.Age

    // ---------- TryFind --------

    [<BenchmarkCategory("TryFind - calculationFunction");Benchmark(Baseline=true)>]
    member this.ArrayTryFind() = this.ArrayWithItems |> Array.tryFind (fun x -> (complexLogic x) = float 15 || x.IsMiddle)
    
    [<BenchmarkCategory("TryFind - calculationFunction");Benchmark>]
    member this.PlinqTryFind() = this.ArrayWithItems.AsParallel().FirstOrDefault(fun x -> (complexLogic x) = float 15 || x.IsMiddle)
        
    [<BenchmarkCategory("TryFind - calculationFunction");Benchmark>]
    member this.ArrayParallelTryFind() = this.ArrayWithItems |> Array.Parallel.tryFind (fun x -> (complexLogic x) = float 15 || x.IsMiddle) 
    
    // ---------- GroupBy --------

    [<BenchmarkCategory("GroupBy - field only");Benchmark(Baseline=true)>]
    member this.ArrayGroupBy() = this.ArrayWithItems |> Array.groupBy _.Age
    
    [<BenchmarkCategory("GroupBy - field only");Benchmark>]
    member this.PlinqGroupBy() = 
        this.ArrayWithItems
            .AsParallel()
            .GroupBy(fun x -> x.Age)            
            .Select(fun x -> x.Key,x.ToArray())
            .ToArray()
        
    [<BenchmarkCategory("GroupBy - field only");Benchmark>]
    member this.ArrayParallelGroupBy() = this.ArrayWithItems |> Array.Parallel.groupBy _.Age  
    
    // ---------- GroupBy complex logic --------

    [<BenchmarkCategory("GroupBy - calculation");Benchmark(Baseline=true)>]
    member this.ArrayGroupBy2() = this.ArrayWithItems |> Array.groupBy (fun x -> (complexLogic x |> hash) % 32)
    
    [<BenchmarkCategory("GroupBy - calculation");Benchmark>]
    member this.PlinqGroupBy2() = 
        this.ArrayWithItems
            .AsParallel()
            .GroupBy(fun x -> (complexLogic x |> hash) % 32)
            .Select(fun x -> x.Key,x.ToArray())
            .ToArray()
        
    [<BenchmarkCategory("GroupBy - calculation");Benchmark>]
    member this.ArrayParallelGroupBy2() = this.ArrayWithItems |> Array.Parallel.groupBy (fun x -> (complexLogic x |> hash) % 32)

        
   
    
    // ---------- Sorting --------

    [<BenchmarkCategory("SortBy - calculation");Benchmark(Baseline=true)>]
    member this.ArraySortBy() = this.ArrayWithItems |> Array.sortBy complexLogic
    
    [<BenchmarkCategory("SortBy - calculation");Benchmark>]
    member this.PlinqSortBy() = this.ArrayWithItems.AsParallel().OrderBy(fun x -> complexLogic x).ToArray()
        
    [<BenchmarkCategory("SortBy - calculation");Benchmark>]
    member this.ArrayParallelSortBy() = this.ArrayWithItems |> Array.Parallel.sortBy complexLogic

    // ---------- Sorting --------

    [<BenchmarkCategory("Sort - by int field");Benchmark(Baseline=true)>]
    member this.ArraySort() = this.ArrayWithItems |> Array.sortBy _.Age
    
    [<BenchmarkCategory("Sort - by int field");Benchmark>]
    member this.PlinqSort() = this.ArrayWithItems.AsParallel().OrderBy(fun x -> x.Age).ToArray()
        
    [<BenchmarkCategory("Sort - by int field");Benchmark>]
    member this.ArrayParallelSort() = this.ArrayWithItems |> Array.Parallel.sortBy _.Age


let run() = 
    BenchmarkRunner.Run<ArrayProcessingBenchmark>() |> ignore
    0
