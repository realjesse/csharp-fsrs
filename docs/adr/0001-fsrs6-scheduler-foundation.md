# ADR 0001: FSRS-6 scheduler foundation

Status: Accepted

## Context

The repository needs a native scheduler with recognizable py-fsrs concepts and a small .NET API.
Correctness and reproducibility come before optimization or infrastructure integrations.

## Decision

- Retain the existing .NET 10 target, Scheduler project, and xUnit test project. No runtime packages are added.
- Use the FSRS namespace, with Scheduler, Card, Rating, State, FsrsParameters, ReviewLog, and ReviewResult.
- Support FSRS-6's 21 weights and parameter bounds. Reference py-fsrs commit
  [9446cb06605c597a063aeee49f7d188d42e34dc2](https://github.com/open-spaced-repetition/py-fsrs/tree/9446cb06605c597a063aeee49f7d188d42e34dc2).
- Card snapshots and review logs are immutable records with validated constructors.
  The caller supplies a long card identifier and DateTimeOffset instants; the library normalizes to UTC.
  There is no clock access, ID generation, storage, or shared random state.
- ReviewCard returns a ReviewResult which supports deconstruction into a card and a log.
  Configuration is immutable and caller collections are defensively copied.
- Use double precision, minimum stability 0.001 days, difficulty in [1, 10],
  completed 24-hour periods for elapsed days, and midpoint-to-even rounding for whole-day intervals.
- Retain the reference default weights, retention 0.9, learning steps of 1 and 10 minutes,
  relearning step of 10 minutes, and maximum review interval of 36500 days.
- Fuzzing is disabled by default. A caller may supply a uniform sample in [0, 1) to each review.
  Its transformation matches the reference, including inclusive-width sampling and rounding.
  This keeps the scheduler deterministic and safe to share without a random generator.
- Reject invalid or incomplete restored memory states, nonfinite configuration, and reviews
  before the previous review. Simultaneous reviews are allowed; retrievability before the last
  review clamps elapsed time to zero.
- Require positive steps up to 36500 days and maximum review intervals from 1 through 36500.
  Steps need not be ascending. Date arithmetic outside DateTimeOffset's range throws rather
  than silently truncating the schedule.
- Compare exact intervals and state transitions against reference tests. The upstream rounded
  memory-state vector uses absolute tolerance 1e-4; analytic double checks use 1e-12.
  Python is not required to build or run tests.

## Consequences

The initial API resembles py-fsrs but intentionally requires explicit identity and time,
accepts equivalent non-UTC offsets, validates restored state more strictly, and defaults to
deterministic scheduling. Review duration is a TimeSpan rather than integer milliseconds.
Configuration bounds are stricter than py-fsrs for intervals and steps.

Card and ReviewLog work with System.Text.Json through their public constructors; no custom
serialization API or Python JSON wire compatibility is promised. Parameter optimization,
history replay, and scheduler serialization remain future work. This is an initial public API,
not a stable package contract.

## Compatibility evidence

The tests adapt test_review_card, test_memo_state, and test_fuzz from the pinned upstream
[tests/test_basic.py](https://github.com/open-spaced-repetition/py-fsrs/blob/9446cb06605c597a063aeee49f7d188d42e34dc2/tests/test_basic.py).
Additional tests cover all ratings, learning/relearning transitions, disabled and shortened
steps, same-day reviews, elapsed-day boundaries, retention, interval caps, immutability,
validation, UTC normalization, and snapshot serialization. These are initial compatibility
checks, not a claim of exhaustive parity across all possible histories.
