module Printing


[<Literal>] 
let formatBody = "(%f,%f)"
[<Literal>] 
let formatPrefix = "Person at coordinates"
[<Literal>] 
let fullFormat = formatPrefix + formatBody

let renderedText = sprintf fullFormat 0.25 0.75
