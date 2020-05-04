﻿using System.Threading.Tasks;
using NServiceBus;
using Verify.NServiceBus;
using VerifyXunit;
using Xunit;
using Xunit.Abstractions;

public class MessageToHandlerMapTests :
    VerifyBase
{
    [Fact]
    public async Task Integration()
    {
        #region MessageToHandlerMap
        var map = new MessageToHandlerMap();
        map.AddMessagesFromAssembly<MyMessage>();
        map.AddHandlersFromAssembly<MyHandler>();
        await Verify(map);
        #endregion
    }

    [Fact]
    public Task AddMessage_type()
    {
        var map = new MessageToHandlerMap();
        map.AddMessage<MyMessage>();
        return VerifyMap(map);
    }

    [Fact]
    public Task AddMessage_assembly()
    {
        var map = new MessageToHandlerMap();
        map.AddMessagesFromAssembly<MyMessage>();
        return VerifyMap(map);
    }

    [Fact]
    public Task AddHandler_type()
    {
        var map = new MessageToHandlerMap();
        map.AddHandler<MyHandler>();
        return VerifyMap(map);
    }

    [Fact]
    public Task AddHandler_assembly()
    {
        var map = new MessageToHandlerMap();
        map.AddHandlersFromAssembly<MyHandler>();
        return VerifyMap(map);
    }

    Task VerifyMap(MessageToHandlerMap map)
    {
        return Verify(new {map.HandledMessages, map.Messages});
    }

    public MessageToHandlerMapTests(ITestOutputHelper output) :
        base(output)
    {
    }

    class MyMessage : IMessage
    {

    }

    class MessageWithNoHandler : IMessage
    {

    }

    class MyHandler : IHandleMessages<MyMessage>
    {
        public Task Handle(MyMessage message, IMessageHandlerContext context)
        {
            return null!;
        }
    }
}