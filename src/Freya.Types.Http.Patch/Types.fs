﻿namespace Freya.Types.Http.Patch

open Freya.Types
open Freya.Types.Http
open FParsec

(* RFC 5789

   Types, parsers and formatters implemented to mirror the specification of 
   Patch semantics as defined in RFC 5789.

   Taken from [http://tools.ietf.org/html/rfc6454] *)

(* Accept-Patch

   Taken from RFC 5789, Section 3.1 Accept-Patch
   See [https://tools.ietf.org/html/rfc5789#section-3.7]
   
   NOTE: RFC 5789 refers to the mediatypes acceptable using the definitions
   current within RFC 2616, the original HTTP standard. This has been
   superceded and here it has been decided to use the more modern formulation
   of mediatypes as defined in RFC 7231. *)

type AcceptPatch =
    | AcceptPatch of MediaType list

    static member Mapping =

        let acceptPatchP =
            Grammar.infix MediaType.Mapping.Parse (skipChar ',') |>> AcceptPatch

        let acceptPatchF =
            function | AcceptPatch x -> Formatting.join MediaType.Mapping.Format (Formatting.append ",") x

        { Parse = acceptPatchP
          Format = acceptPatchF }

    static member format =
        Mapping.format AcceptPatch.Mapping

    static member parse =
        Mapping.parse AcceptPatch.Mapping

    static member tryParse =
        Mapping.tryParse AcceptPatch.Mapping

    override x.ToString () =
        AcceptPatch.format x

(* Methods

   Shorthand for the custom HTTP Method with the value PATCH, not a formal
   part of an RFC, but provided for convenience. *)

[<AutoOpen>]
module Methods =

    let PATCH =
        Method.Custom "PATCH"