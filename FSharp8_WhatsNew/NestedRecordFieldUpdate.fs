module NestedRecordFieldUpdate

type AnotherNestedRecTy = { A: int }
type NestdRecTy = { B: AnotherNestedRecTy; C: string }
type RecTy = { D: NestdRecTy; E: string option }

let beforeThisFeature x = 
    { x with D = {x.D with 
                    B = {x.D.B with A = 1}
                    C = "ads"
                  }
    }

// ------------------- //
let withTheFeature x = { x with D.B.A = 1; D.C = "ads" }
let alsoWorksForAnonymous (x:RecTy) = {| x with D.C = "anon"; Y = "new field!" |}

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
let codeWhichWorks = {oneBook with Book.Author.Name = "Author1Updated"}
(*
let codeWhichLeadsToAnError = {oneBook with Author.Name = "Author1Updated"}
*)