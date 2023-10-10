module ArithmeticLiterals

let [<Literal>] bytesInKB = 2f ** 10f
let [<Literal>] bytesInMB = bytesInKB * bytesInKB
let [<Literal>] bytesInGB = 1 <<< 30
let [<Literal>] customBitMask = 0b00000101uy
let [<Literal>] inverseBitMasp = ~~~ customBitMask

