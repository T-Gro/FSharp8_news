module WarnIfObj

module Assert =
    let AreEqual (expected : obj, actual : obj) = ()

let a = List.empty
Assert.AreEqual(List.empty, a)