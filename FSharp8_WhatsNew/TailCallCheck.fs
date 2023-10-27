module TailCallCheck

let mul x y = x * y


[<TailCall>]
let rec factorialClassic n =
    match n with
    | 0u | 1u -> 1u
    | _ -> n * (factorialClassic (n - 1u))

[<TailCall>]
let rec factorialWithAcc n accumulator = 
    match n with
    | 0u | 1u -> accumulator
    | _ -> factorialWithAcc (n - 1u) (n * accumulator)

let fact n = factorialWithAcc n 1u