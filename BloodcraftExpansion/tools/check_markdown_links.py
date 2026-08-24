"""Validate local Markdown links and image references in the Bloodcraft wiki."""

from __future__ import annotations

import re
import sys
from pathlib import Path
from urllib.parse import unquote


DOCS = Path(__file__).resolve().parents[1] / "docs"
LINK_RE = re.compile(r"!?\[[^\]]*\]\((?P<target><[^>]+>|[^)\s]+)(?:\s+['\"].*?['\"])?\)")


def main() -> int:
    failures: list[str] = []
    checked = 0
    pages = sorted(DOCS.rglob("*.md"))
    graph: dict[Path, set[Path]] = {page.resolve(): set() for page in pages}

    for source in pages:
        text = source.read_text(encoding="utf-8")
        for match in LINK_RE.finditer(text):
            target = match.group("target").strip("<>")
            if target.startswith(("http://", "https://", "mailto:", "#")):
                continue

            path_text = unquote(target.split("#", 1)[0])
            if not path_text:
                continue

            checked += 1
            resolved = (source.parent / path_text).resolve()
            try:
                resolved.relative_to(DOCS.resolve())
            except ValueError:
                failures.append(f"{source.relative_to(DOCS)}: target escapes docs: {target}")
                continue

            if not resolved.exists():
                failures.append(f"{source.relative_to(DOCS)}: missing {target}")
            elif resolved.suffix.lower() == ".md":
                graph[source.resolve()].add(resolved)

    home = (DOCS / "HOME.md").resolve()
    reachable: set[Path] = set()
    pending = [home]
    while pending:
        page = pending.pop()
        if page in reachable:
            continue
        reachable.add(page)
        pending.extend(graph.get(page, set()) - reachable)

    for orphan in sorted(set(graph) - reachable):
        failures.append(f"{orphan.relative_to(DOCS.resolve())}: not reachable from HOME.md")

    if failures:
        print("Markdown link validation failed:")
        print("\n".join(f"- {failure}" for failure in failures))
        return 1

    print(f"Validated {checked} local links and reachability across {len(pages)} Markdown pages.")
    return 0


if __name__ == "__main__":
    sys.exit(main())
