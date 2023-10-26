module ArithmeticLiterals

open System
module ArithmeticLiteralsBefore =
    let [<Literal>] bytesInKB = 1024f
    let [<Literal>] bytesInMB = 1048576f
    let [<Literal>] bytesInGB = 1073741824
    let [<Literal>] customBitMask =  0b01010101uy
    let [<Literal>] inverseBitMask = 0b10101010uy

    type MyEnum = 
        | A = 32
        | B = 11
        | C = bytesInGB

let [<Literal>] bytesInKB = 2f ** 10f
let [<Literal>] bytesInMB = bytesInKB * bytesInKB
let [<Literal>] bytesInGB = 1 <<< 30
let [<Literal>] customBitMask = 0b01010101uy
let [<Literal>] inverseBitMask = ~~~ customBitMask


type MyEnum = 
    | A = (1 <<< 5)
    | B = (17 * 45 % 13)
    | C = bytesInGB

[<System.Runtime.CompilerServices.MethodImplAttribute(enum(1+2+3))>]
let doStuff = ()