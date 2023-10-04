using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Xunit;

namespace TBureck.BuildingBlocks.Domain;

public class EventDrivenAggregateTest
{

    [Fact]
    public void Apply_AddsToChanges()
    {
        ExampleEventDrivenAggregate actual = new();
        actual.SomethingHappens();
        
        Assert.Collection(actual.Changes, @event => Assert.IsType<DummyEventHappened>(@event));
    }

    [Fact]
    public void Apply_Mutates()
    {
        ExampleEventDrivenAggregate actual = new();
        actual.SomethingHappens();
        
        Assert.True(actual.SomethingHappened);
    }

    [Fact]
    public void Apply_RaisesEvent()
    {
        bool somethingHappened = false;
        Event<DummyEventHappened>.Raised += _ => somethingHappened = true;

        ExampleEventDrivenAggregate actual = new();
        actual.SomethingHappens();
        
        Assert.True(somethingHappened);
    }

    [Fact]
    public void Apply_DoesNotChangeWhenExceptionOccurs()
    {
        bool somethingHappened = false;
        Event<FailingDummyEventHappened>.Raised += _ => somethingHappened = true;

        ExampleEventDrivenAggregate actual = new();
        Assert.Throws<Exception>(() => actual.SomethingFails());
        
        Assert.False(somethingHappened);
        Assert.Empty(actual.Changes);
    }

    [Fact]
    public void Mutate_Mutates()
    {
        ExampleEventDrivenAggregate actual = new();
        actual.Mutate(new DummyEventHappened());
        
        Assert.True(actual.SomethingHappened);
    }

    [Fact]
    public void Mutate_DoesNotAddChange()
    {
        ExampleEventDrivenAggregate actual = new();
        actual.Mutate(new DummyEventHappened());
        
        Assert.Empty(actual.Changes);
    }
    
    public class DummyEventHappened : IDomainEvent { }
    
    public class FailingDummyEventHappened : IDomainEvent { }

    public class ExampleEventDrivenAggregate : IEventDrivenAggregate
    {

        public List<IDomainEvent> Changes { get; } = new();
    
        public bool SomethingHappened { get; private set; }

        public ExampleEventDrivenAggregate()
        {
            SomethingHappened = false;
        }

        public void SomethingHappens()
        {
            this.Apply(new DummyEventHappened());
        }

        public void SomethingFails()
        {
            this.Apply(new FailingDummyEventHappened());
        }

        [PublicAPI]
        public void When(DummyEventHappened @event)
        {
            SomethingHappened = true;
        }

        [PublicAPI]
        public void When(FailingDummyEventHappened @event)
        {
            throw new Exception();
        }
    }
}
