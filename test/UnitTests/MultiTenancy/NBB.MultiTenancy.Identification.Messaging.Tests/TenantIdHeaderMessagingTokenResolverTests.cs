﻿// Copyright (c) TotalSoft.
// This source code is licensed under the MIT license.

using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using NBB.Messaging.Abstractions;
using Xunit;

namespace NBB.MultiTenancy.Identification.Messaging.Tests
{
    public class TenantIdHeaderMessagingTokenResolverTests
    {
        private readonly MessagingContextAccessor _mockMessagingContextAccessor;
        private readonly Dictionary<string, string> _headers;

        public TenantIdHeaderMessagingTokenResolverTests()
        {
            _mockMessagingContextAccessor = new MessagingContextAccessor();
            _headers = new Dictionary<string, string>();
            var mockMessagingEnvelope = new MessagingEnvelope(_headers, new object());
            var mockMessagingContext = new MessagingContext(mockMessagingEnvelope, "topic", null);

            _mockMessagingContextAccessor.MessagingContext = mockMessagingContext;
        }

        [Fact]
        public async Task Should_Resolve_Token_FromHeader()
        {
            // Arrange
            const string key = "test token key";
            const string value = "test token value";
            _headers.Add(key, value);
            var sut = new TenantIdHeaderMessagingTokenResolver(_mockMessagingContextAccessor, key);

            // Act
            var result = await sut.GetTenantToken();

            // Assert
            result.Should().Be(value);
        }

        [Fact]
        public async Task Should_Return_Null_For_Bad_Keys()
        {
            // Arrange
            const string key = "bad token key";
            var sut = new TenantIdHeaderMessagingTokenResolver(_mockMessagingContextAccessor, key);

            // Act
            var result = await sut.GetTenantToken();

            // Assert
            result.Should().BeNull();
        }
    }
}
