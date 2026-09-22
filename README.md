# FSRS for .NET

A native C# implementation of the Free Spaced Repetition Scheduler (FSRS) for the .NET runtime.

The project aims to provide production-ready scheduling and parameter optimization without requiring Python.NET, native binaries, or another language runtime. It will follow the established FSRS algorithm while offering an idiomatic, well-tested .NET API.

> **Status:** Early design. The public API, package names, and supported FSRS version are not yet stable.

## Goals

- Native managed C# implementation.
- FSRS scheduling and parameter optimization.
- Behavioral compatibility with trusted FSRS implementations.
- Clear, minimal, and stable public APIs.
- Strong automated testing and reproducible benchmarks.
- Distribution through NuGet.

## Non-goals

This repository is a library, not a complete flashcard application. It does not provide user interfaces, persistence, synchronization, authentication, or hosted services.

## Documentation

- [Architecture](docs/architecture.md)
- [Architectural decisions](docs/adr/)

## Development

The solution structure, build commands, and contribution workflow will be documented once the initial implementation is established. Until then, design changes should remain small and be recorded in the architecture document or an ADR when they create a lasting constraint.

## Contributing

Issues and pull requests are welcome. Contributions should include tests, preserve documented compatibility, and avoid expanding the public API without a demonstrated use case.

## License and attribution

The license and detailed upstream attribution will be added before the first release. FSRS originates from the open-spaced-repetition community, whose specifications and implementations provide essential behavioral references for this project.
