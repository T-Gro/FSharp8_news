module WhileBang

let mutable count = 0
let asyncCondition = async {
    return count < 10
}

let doStuffBeforeThisFeature = 
    async {
       let! firstRead = asyncCondition
       let mutable read = firstRead
       while read do
         count <- count + 2
         let! nextRead = asyncCondition
         read <- nextRead
       return count
    }

module WithFsharp8 = 

    let doStuffWithWhileBang =
        async {
            while! asyncCondition do
                count <- count + 2
            return count
        }
