module Printing


let redenredCoordinatesOld = sprintf "(%f,%f)" 0.25 0.75
let renderedTextOld = sprintf "Person at coordinates(%f,%f)" 0.25 0.75

module WithFsharp8 = 

    [<Literal>] 
    let formatBody = "(%f,%f)"
    [<Literal>] 
    let formatPrefix = "Person at coordinates"
    [<Literal>] 
    let fullFormat = formatPrefix + formatBody

    let renderedText = sprintf fullFormat 0.25 0.75
