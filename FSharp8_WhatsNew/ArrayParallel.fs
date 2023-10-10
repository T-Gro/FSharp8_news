module ArrayParallel

let arr = [|1..100_000_000|]
arr |> Array.Parallel.sortInPlace