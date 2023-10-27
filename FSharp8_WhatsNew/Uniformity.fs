module Uniformity

[<Interface>]
type IDemoableOld =
    abstract member Show: string -> unit

module IDemoableOld =
    let autoFormat(a) = sprintf "%A" a

[<Interface>]
type IDemoable =
    abstract member Show: string -> unit
    static member AutoFormat(a) = sprintf "%A" a

let txt = IDemoable.AutoFormat (42,42)

open System.Threading
open FSharp.Reflection

type AbcDU = A | B | C
    with   
        static let namesAndValues = 
            FSharpType.GetUnionCases(typeof<AbcDU>) 
            |> Array.map (fun c -> c.Name, FSharpValue.MakeUnion (c,[||]) :?> AbcDU)
        static let stringMap = namesAndValues |> dict
        static let mutable cnt = 0
        
        static do printfn "Init done! We have %i cases" stringMap.Count
        static member TryParse text = 
            let cnt = Interlocked.Increment(&cnt)
            stringMap.TryGetValue text, sprintf "Parsed %i" cnt

type AnotherDu = D | E
    with
        //static val mutable private myXX : int
        static member  val X = 42 with get,set
         
        

AbcDU.TryParse "xxx"
AbcDU.TryParse "A"
AbcDU.TryParse "B"

let rec f () = seq {
    try 
        yield 123    
        yield (456/0)
    with exn ->
        eprintfn "%s" exn.Message
        yield 789
        yield! f()
}

let first5 = 
    f() 
    |> Seq.take 5 
    |> Seq.toArray

let sum =
    [ for x in [0;1] do       
            try          
                yield 1              
                yield (10/x)    
                yield 100  
            with _ ->
                yield 1000 ]
    |> List.sum