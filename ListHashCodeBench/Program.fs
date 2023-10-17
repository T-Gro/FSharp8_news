// For more information see https://aka.ms/fsharp-console-apps

open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Running
open BenchmarkDotNet.Configs
open BenchmarkDotNet.Columns
open BenchmarkDotNet.Order
open BenchmarkDotNet.Jobs


type MyConfigWithNugets() as this =
    inherit ManualConfig()
    //do this.AddJob(Job.Default.WithNuGet(NugetConfig.nugetList1)) |> ignore
    //do this.AddJob(Job.Default.WithNuGet(NugetConfig.nugetList2)) |> ignore

[<MemoryDiagnoser>]
[<InProcessAttribute>]
[<CategoriesColumn>]
[<Config(typeof<MyConfigWithNugets>)>]
[<Orderer(SummaryOrderPolicy.SlowestToFastest, MethodOrderPolicy.Declared)>]
type ListHashCodeBenchMark() =

    member val ListWithItems = Unchecked.defaultof<int list> with get,set
    [<Params(15_000)>] 
    member val NumberOfItems = -1 with get,set

    [<GlobalSetup>]
    member this.GlobalSetup () = 
        this.ListWithItems <- List.init this.NumberOfItems id

    [<Benchmark>]
    member this.HashCodeOfList() = this.ListWithItems.GetHashCode()

[<EntryPoint>]
let run argv = 
    BenchmarkRunner.Run<ListHashCodeBenchMark>() |> ignore
    0
