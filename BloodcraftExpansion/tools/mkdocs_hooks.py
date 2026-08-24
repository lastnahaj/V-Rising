"""MkDocs build hooks for the Bloodcraft wiki."""

from pathlib import Path, PurePosixPath

from mkdocs.structure.files import File


def on_files(files, config, **kwargs):
    """Publish HOME.md at the root and normalize documentation URLs."""
    docs_dir = Path(config.docs_dir)
    home = next(file for file in files if file.src_uri == "HOME.md")
    files.remove(home)

    index = File.generated(
        config,
        "index.md",
        content=(docs_dir / "HOME.md").read_text(encoding="utf-8"),
    )
    index.edit_uri = "BloodcraftExpansion/docs/HOME.md"
    files.append(index)

    for file in files:
        source = PurePosixPath(file.src_uri)
        if source.suffix.lower() != ".md" or source.name == "index.md":
            continue

        if source.name.lower() == "readme.md":
            destination = source.parent / "index.html"
        else:
            slug = source.stem.lower()
            if file.src_uri == "custom/CUSTOM-CLASSES.md":
                slug = "classes"
            destination = source.parent / slug / "index.html"

        file.dest_uri = destination.as_posix()

    return files


def on_page_markdown(markdown, **kwargs):
    """Retarget source HOME.md links to the generated site index."""
    return markdown.replace("HOME.md)", "index.md)")
