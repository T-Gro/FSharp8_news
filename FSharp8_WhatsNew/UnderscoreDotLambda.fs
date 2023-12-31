﻿module UnderscoreDotLambda

type Person = {Name : string; Age : int}
let people = 
    [ {Name = "Joe"; Age = 20}
      {Name = "Will"; Age = 30}
      {Name = "Joe"; Age = 51} ]

let beforeThisFeature = 
    people 
    |> List.distinctBy (fun x -> x.Name)
    |> List.groupBy (fun x -> x.Age)
    |> List.map (fun (x,y) -> y)
    |> List.map (fun x -> x.Head.Name)
    |> List.sortBy (fun x -> x.ToString())

module WithFsharp8 = 

    let possibleNow = 
        people 
        |> List.distinctBy _.Name
        |> List.groupBy _.Age
        |> List.map snd
        |> List.map _.Head.Name
        |> List.sortBy _.ToString()

    let ageAccessor : Person -> int = _.Age
    let getNameLength = _.Name.Length

    type XXX = {WhatANiceProperty : string}

    let inline myPropGetter (x: 'a when 'a:(member WhatANiceProperty:string)) = 
        x |> _.WhatANiceProperty