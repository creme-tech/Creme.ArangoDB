namespace FSharp.ArangoDB

module Types =
    open FSharp.Json
    open System.Net.Http

    type Collection =
        { isSystem: bool
          name: string
          status: int
          [<JsonField "type">]
          type': int }

    type CollectionKeyOptions =
        { [<JsonField "type">]
          type': string }

    type CollectionOptions =
        { name: string
          keyOptions: CollectionKeyOptions
          [<JsonField "type">]
          type': int }

    type IndexOptions =
        { fields: string list
          [<JsonField "type">]
          type': string
          unique: bool }

    type Query<'T> =
        { bindVars: Map<string, 'T>
          query: string }

    type QueryResult<'T> = { result: 'T }

    type ViewLinks = { includeAllFields: bool }

    type ViewOptions =
        { links: Map<string, ViewLinks>
          name: string
          [<JsonField "type">]
          type': string }

    type Client =
        { authorization: string
          client: HttpClient
          database: string
          debug: bool
          host: string
          target: string }
