#!/usr/bin/env python3
"""Validate release metadata and high-value cross-page wiki invariants."""

from __future__ import annotations

import re
import sys
from pathlib import Path


ROOT = Path(__file__).resolve().parents[2]
DOCS = ROOT / "BloodcraftExpansion" / "docs"
VERSION = "1.13.26"
VERIFIED = "2026-11-02"


def fail(errors: list[str], path: Path, message: str) -> None:
    errors.append(f"{path.relative_to(ROOT).as_posix()}: {message}")


def main() -> int:
    errors: list[str] = []
    pages = sorted(DOCS.rglob("*.md"))

    for path in pages:
        text = path.read_text(encoding="utf-8")
        if '!!! version "Upstream Bloodcraft Reference"' in text:
            if f"**Bloodcraft version:** {VERSION} stable" not in text:
                fail(errors, path, f"missing Bloodcraft {VERSION} version banner")
            if f"**Last verified:** {VERIFIED}" not in text:
                fail(errors, path, f"missing {VERIFIED} verification date")

        if path.name not in {"CHANGELOG.md"} and "PrimalEchoItemPrefabGUID" in text:
            fail(errors, path, "uses obsolete Primal Echo configuration key")
        if re.search(r"`\.quest get(?:\s|`)", text):
            fail(errors, path, "uses nonexistent .quest get command")

    commands = (DOCS / "reference" / "COMMANDS.md").read_text(encoding="utf-8")
    command_rows = re.findall(r"^\| `\.[^\n]+\| (?:Yes|No) \|$", commands, re.MULTILINE)
    if len(command_rows) != 88:
        fail(errors, DOCS / "reference" / "COMMANDS.md", f"expected 88 commands, found {len(command_rows)}")

    config_pages = sorted((DOCS / "reference").glob("CONFIG-*.md"))
    config_pages = [path for path in config_pages if path.name != "CONFIGURATION.md"]
    config_rows = 0
    for path in config_pages:
        text = path.read_text(encoding="utf-8")
        config_rows += len(re.findall(r"^\| `[^`]+` \|", text, re.MULTILINE))
    if config_rows != 153:
        fail(errors, DOCS / "reference" / "CONFIGURATION.md", f"expected 153 settings, found {config_rows}")

    exact_types = {
        "CONFIG-GENERAL.md": {"LanguageLocalization": "string (enum name)"},
        "CONFIG-EXPERTISE.md": {"UnarmedSlots": "bool", "FishingSlots": "bool"},
        "CONFIG-FAMILIARS.md": {"ShinyChance": "float"},
        "CONFIG-PROFESSIONS.md": {"ExtraRecipes": "bool"},
        "CONFIG-QUESTS.md": {"RerollDailyAmount": "int", "RerollWeeklyAmount": "int"},
    }
    for filename, settings in exact_types.items():
        path = DOCS / "reference" / filename
        text = path.read_text(encoding="utf-8")
        for key, expected_type in settings.items():
            pattern = rf"^\| `{re.escape(key)}` \| {re.escape(expected_type)} \|"
            if not re.search(pattern, text, re.MULTILINE):
                fail(errors, path, f"{key} must use type {expected_type}")

    required_pages = [
        DOCS / "custom" / "ARCHITECTURE.md",
        DOCS / "custom" / "DESIGN-STANDARDS.md",
        DOCS / "custom" / "ROADMAP.md",
    ]
    for path in required_pages:
        if not path.is_file():
            fail(errors, path, "required expansion-governance page is missing")

    if errors:
        print("Wiki consistency validation failed:", file=sys.stderr)
        for error in errors:
            print(f"- {error}", file=sys.stderr)
        return 1

    print(
        f"Validated {len(pages)} Markdown pages, {len(command_rows)} commands, "
        f"{config_rows} settings, and Bloodcraft {VERSION} release metadata."
    )
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
