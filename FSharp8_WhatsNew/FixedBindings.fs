module FixedBindings

open System
open FSharp.NativeInterop

#nowarn "9"
let pinIt (span: Span<char>, byRef: byref<int>, inRef: inref<int>) =
    // Calls span.GetPinnableReference()
    use ptrSpan = fixed span
    use ptrByRef = fixed &byRef
    use ptrInref = fixed &inRef
    
    NativePtr.copyBlock ptrByRef ptrInref 1



