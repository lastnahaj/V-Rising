# Changelog

## 1.0.0

- Added two-way V Rising global chat and Discord chat with centralized sanitization and cooldown enforcement.
- Added cached server and player state shared by all Discord features.
- Added player join and reconnect-suppressed leave notifications.
- Added optional player death, PvP, grouped V Blood, and castle-breach feeds.
- Added live Discord bot presence with population templates.
- Added a persistent editable server-status embed with automatic message-ID persistence.
- Added an optional debounced and lockable voice-channel player counter.
- Added `/status`, `/players`, `/player`, `/announce`, and `/relay-status` guild commands.
- Added centralized Discord role authorization and administrative audit logging.
- Added isolated Discord queuing, retry backoff, clean shutdown, configuration validation, and secure token environment-variable support.
- Added bounded cancellable game-thread dispatch with pending and rejection health metrics.
- Added complete outcome auditing for `/player`, `/announce`, and `/relay-status`, including delivery health.
- Added permission-preserving voice-channel locking and bounded final offline publication during clean shutdown.
- Added interval timestamp refreshes, centralized Discord display sanitization, and explicit third-party bot filtering.
- Added non-destructive reconciliation of only SanguineRelay-owned guild commands.
- Added world-initialization uptime, locked dependency graphs, verified bootstrap hashes, and release privacy checks.
