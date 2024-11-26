// <copyright file="Query.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Rx.Tracker.Core.Mediation.Queries;

/// <summary>
/// Represents a query made to the mediator.
/// </summary>
/// <typeparam name="TResult">The result type.</typeparam>
public interface IQuery<TResult> : IRequest<TResult>;