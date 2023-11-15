module NestedRecordFieldUpdate

type SteeringWheel = { Type: string }
type CarInterior = { Steering: SteeringWheel; Seats: int }
type Car = { Interior: CarInterior; ExteriorColor: string option }

let beforeThisFeature x = 
    { x with Interior = { x.Interior with 
                            Steering = {x.Interior.Steering with Type = "yoke"}
                            Seats = 5
                        }
    }

module WithFsharp8 = 
    let withTheFeature x = 
        { x with Interior.Steering.Type = "yoke"
                 Interior.Seats = 5 }

    let alsoWorksForAnonymous (x:Car) = 
        {| x with Interior.Seats = 7
                  Price = 99_999 |}

    module TypeNamePrefix = 
        type Author = {
            Name: string
            YearBorn: int
        }
        type Book = {
            Title: string
            Year: int
            Author: Author
        }
        let oneBook = { Title = "Book1"; Year = 2000; Author = { Name = "Author1"; YearBorn = 1950 } }
        let usedToWorkBefore = {oneBook with Book.Year = 2022}
        let codeWhichWorksToday = {oneBook with Book.Author.Name = "Author1Updated"}
  