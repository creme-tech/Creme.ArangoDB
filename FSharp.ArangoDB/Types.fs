namespace FSharp.ArangoDB

module Types =
    open FSharp.Json
    open System.Net.Http

    type Collection =
        { isSystem: bool
          name: string
          status: int
          [<JsonField "type">]
          _type: int }

    type CollectionKeyOptions =
        { [<JsonField "type">]
          _type: string }

    type CollectionOptions =
        { name: string option
          keyOptions: CollectionKeyOptions
          [<JsonField "type">]
          _type: int }

    type IndexOptions =
        { fields: string list
          [<JsonField "type">]
          _type: string
          unique: bool }

    type Query<'T> =
        { batchSize: int
          bindVars: Map<string, 'T>
          query: string option }

    type QueryResult<'T> = { id: string option; result: 'T }

    type SearchLinks = { includeAllFields: bool }

    type SearchOptions =
        { links: Map<string, SearchLinks>
          name: string option
          [<JsonField "type">]
          _type: string }

    type Client =
        { authorization: string
          client: HttpClient
          database: string
          debug: bool
          host: string
          target: string }
