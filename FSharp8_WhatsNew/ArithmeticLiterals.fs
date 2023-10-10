module ArithmeticLiterals

let [<Literal>] bytesInKB = 2f ** 10f
let [<Literal>] bytesInMB = bytesInKB * bytesInKB
let [<Literal>] bytesInGB = 1 <<< 30
let [<Literal>] customBitMask = 0b01010101uy
let [<Literal>] inverseBitMask = ~~~ customBitMask

type MyEnum = 
    | A = (1 <<< 5)
    | B = (17 * 45 % 13)
    | C = bytesInGB