module ConstraintIntersectionSyntax

open System


type IEx =
    abstract h: #IDisposable & #seq<int> -> unit

let beforeThis(arg1 : 't 
    when 't:>IDisposable 
    and 't:>IEx 
    and 't:>seq<int>) =
    arg1.h(arg1)
    arg1.Dispose()
    for x in arg1 do
        printfn "%i" x

let withNewFeature (arg1: 't & #IEx & 
    #IDisposable & #seq<int>) =
    arg1.h(arg1)
    arg1.Dispose()
    for x in arg1 do
        printfn "%i" x


