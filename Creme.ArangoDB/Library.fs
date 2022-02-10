namespace Creme.ArangoDB

module ArangoClient =
    let SetConfig = Client.SetConfig

    let GetCollection = Collection.GetCollection
    let CreateCollection = Collection.CreateCollection

    let GetAccessibleDatabases = Database.GetAccessibleDatabases
    let CreateDatabase = Database.CreateDatabase

    let CreateIndex = Index.CreateIndex

    let Query<'T> = Query'.Query<'T>
    let QueryNext<'T> = Query'.QueryNext<'T>

    let GetSearch = Search.GetSearch
    let CreateSearch = Search.CreateSearch

    let BeginTransaction = Transaction.BeginTransaction
    let CommitTransaction = Transaction.CommitTransaction
