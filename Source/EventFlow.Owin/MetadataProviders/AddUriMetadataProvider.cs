﻿// The MIT License (MIT)
//
// Copyright (c) 2015 Rasmus Mikkelsen
// https://github.com/rasmus/EventFlow
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System.Collections.Generic;
using EventFlow.Aggregates;
using EventFlow.EventStores;
using Microsoft.Owin;

namespace EventFlow.Owin.MetadataProviders
{
    public class AddUriMetadataProvider : IMetadataProvider
    {
        private readonly IOwinContext _owinContext;

        public AddUriMetadataProvider(
            IOwinContext owinContext)
        {
            _owinContext = owinContext;
        }

        public IEnumerable<KeyValuePair<string, string>> ProvideMetadata<TAggregate>(
            IIdentity id,
            IAggregateEvent aggregateEvent,
            IMetadata metadata)
            where TAggregate : IAggregateRoot
        {
            // TODO: Handle X-Forwarded-Proto header

            yield return new KeyValuePair<string, string>("request_uri", _owinContext.Request.Uri.ToString());
            yield return new KeyValuePair<string, string>("request_method", _owinContext.Request.Method.ToUpperInvariant());
        }
    }
}