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

using System;
using EventFlow.Aggregates;
using EventFlow.Bdd.Steps.ThenSteps;
using EventFlow.Configuration;
using EventFlow.Core;

namespace EventFlow.Bdd.Contexts
{
    public class ThenContext : IThenContext
    {
        private readonly IResolver _resolver;
        private IScenarioRunnerContext _context;

        public ThenContext(
            IResolver resolver)
        {
            _resolver = resolver;
        }

        public IThen Event<TAggregate, TIdentity, TAggregateEvent>(TIdentity identity)
            where TAggregate : IAggregateRoot<TIdentity>
            where TIdentity : IIdentity
            where TAggregateEvent : IAggregateEvent<TAggregate, TIdentity>
        {
            return Event<TAggregate, TIdentity, TAggregateEvent>(identity, e => true);
        }

        public IThen Event<TAggregate, TIdentity, TAggregateEvent>(
            TIdentity identity,
            Predicate<IDomainEvent<TAggregate, TIdentity, TAggregateEvent>> predicate)
            where TAggregate : IAggregateRoot<TIdentity>
            where TIdentity : IIdentity
            where TAggregateEvent : IAggregateEvent<TAggregate, TIdentity>
        {
            _context.AddThen(new ValidateEventHappendScenarioStep<TAggregate, TIdentity, TAggregateEvent>(_context, _resolver, identity, predicate));
            return this;
        }

        public void Dispose()
        {
        }

        public void Setup(IScenarioRunnerContext scenarioRunnerContext)
        {
            _context = scenarioRunnerContext;
        }
    }
}