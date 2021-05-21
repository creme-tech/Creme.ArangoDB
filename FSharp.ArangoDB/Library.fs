namespace FSharp.ArangoDB

module ArangoClient =

    (* Client *)

    let SetConfig = Client.SetConfig

    let GetCollection = Collection.GetCollection
    let CreateCollection = Collection.CreateCollection

    let CreateIndex = Index.CreateIndex

    let GetSearch = Search.GetSearch
    let CreateSearch = Search.CreateSearch

    let Query<'T> = Query'.Query<'T>
    let QueryNext<'T> = Query'.QueryNext<'T>
