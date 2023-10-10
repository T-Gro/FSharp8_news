module TailCallCheck

let mul x y = x * y

[<TailCall>]
let rec fact n acc =
    if n = 0
    then acc
    else (fact (n - 1) (mul n acc)) + 23