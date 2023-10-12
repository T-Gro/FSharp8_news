module ArrayParallel

let arr = [|1..100_000_000|]
arr |> Array.Parallel.sortInPlace

let ass = typeof<List<_>>.Assembly
let arrParType = 
    ass.GetTypes() 
    |> Array.find (fun a -> a.Name.Contains("ArrayModule"))
    |> _.GetNestedTypes()
    |> Array.find (fun a -> a.Name.Contains("Parallel"))
    |> _.GetMethods()
    |> Array.filter (fun m -> (m.GetCustomAttributes(false)) |> Array.exists (fun a -> a.ToString().Contains("Experimental")))
    |> Array.filter (fun m -> m.Name.EndsWith("$W") |> not)
    |> Array.iter (fun m -> printfn $" - {m.Name[0..0].ToLower() + m.Name[1..]}")

