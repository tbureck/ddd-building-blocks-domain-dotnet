# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]
### Breaking
- `Event` now needs to be used with implementation of `IDomainEvent`
- Drop support for .NET standard 2.0

### Added
- `TypedGuidId::ToString` returns combination of type and Guid now
- `IEventDrivenAggregate` - interface for building event-driven aggregates
- `IDomainEvent` - marker interface for domain events

## [0.3.0] - 2023-01-21
### Changed
- Updated target framework to .NET 6 LTS version

## [0.2.0] - 2021-06-19
### Added
- `Event` - simple port for handling domain events

## [0.1.0] - 2021-06-12
### Added
- `TypedGuidId` - value object for entity IDs
- `IRepository` - interface for entity repositories