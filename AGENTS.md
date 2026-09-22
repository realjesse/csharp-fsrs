# AGENTS.md

## Project

This repository implements FSRS scheduling and parameter optimization natively in C# for the .NET runtime. It is a reusable library, not an application or hosted service.

## Read First

- [README.md](README.md) — user-facing purpose and project status.
- [docs/architecture.md](docs/architecture.md) — architectural direction and system vocabulary.
- `docs/adr/` — individual architectural decisions once added.

## Priorities

1. Algorithmic correctness and compatibility.
2. A small, idiomatic, stable public API.
3. Clear, maintainable code with explicit domain concepts.
4. Measured performance without premature complexity.

## Working Rules

- Use current repository configuration and existing conventions as the source of truth.
- Keep the FSRS domain independent of frameworks, storage, hosting, and dependency-injection containers.
- Prefer simple types and direct code over speculative abstractions.
- Develop behavior with tests; add regression tests for every corrected defect.
- Compare algorithmic behavior with trusted FSRS references and document numerical tolerances.
- Treat public API changes, compatibility changes, package boundaries, and major dependencies as architectural decisions.
- Update documentation when behavior or a lasting architectural constraint changes.
- Do not commit generated build output, credentials, local configuration, or benchmark artifacts unless explicitly required.

## Completion Check

Before considering a change complete, format the code, build the solution, run the relevant tests, and report any checks that could not be run. Use the repository's documented commands once they exist rather than inventing alternatives.
