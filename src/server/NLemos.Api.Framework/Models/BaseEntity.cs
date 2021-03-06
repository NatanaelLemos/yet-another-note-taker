﻿using System;

namespace NLemos.Api.Framework.Models
{
    /// <summary>
    /// This is the least required to be written into database
    /// </summary>
    public abstract class BaseEntity
    {
        protected BaseEntity()
        {
            Id = Guid.NewGuid();
            Added = DateTimeOffset.UtcNow;
            Modified = DateTimeOffset.UtcNow;
        }

        /// <summary>
        /// The key of the entity.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The DateTime when the entity was first written to database.
        /// </summary>
        /// <remarks>
        ///     See https://docs.microsoft.com/en-us/dotnet/api/system.datetimeoffset?view=netframework-4.8
        ///     and https://stackoverflow.com/a/14268167 for why to use DateTimeOffset
        /// </remarks>
        public DateTimeOffset Added { get; set; }

        public DateTimeOffset Modified { get; set; }
    }
}
