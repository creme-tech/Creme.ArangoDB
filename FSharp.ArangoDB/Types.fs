namespace FSharp.ArangoDB

module Types =
    open System.Net.Http

    type Collection =
        { ID: string
          Name: string
          Status: int
          Type: int
          IsSystem: bool }

    type CollectionKeyOptions = { Type: string }

    type CollectionOptions =
        { Name: string
          KeyOptions: CollectionKeyOptions
          Type: int }

    type Query =
        { Query: string
          BindVars: Map<string, obj> }

    type QueryResult<'T> = { ID: string option; Result: 'T }

    type Client =
        { Authorization: string
          __Client: HttpClient (* Internal *)
          Database: string
          __Debug: bool (* TODO: Debug mode *) (* Internal *)
          Host: string
          __Target: string (* Internal *)  }
