from pathlib import Path

import argparse
import sys


OUTPUT_DIR_PATH: str = "src/Triangulum/components"


def generate_main_class(class_name: str) -> str:
    return f"""using Godot;
using Triangulum.Classes;

namespace Triangulum.Components;

[GlobalClass, Tool]
internal partial class {class_name} : Component
{{
	public {class_name}() : base()
	{{
		#if TOOLS
		if (IsInGroup(GroupName.Components)) return;
		#endif
    }}
}}
"""


def generate_properties_class(class_name: str) -> str:
    return f"""using Godot;

namespace Triangulum.Components;

internal partial class {class_name}
{{
    #region Main
    #endregion // Main
}}
"""


def generate_setter_hooks_class(class_name: str) -> str:
    return f"""using Godot;

namespace Triangulum.Components;

internal partial class {class_name}
{{
    #region Main
    #endregion // Main
}}
"""


def create_class_files(class_name: str) -> None:
    files: dict[str, str] = {
        f"{class_name}.cs": generate_main_class(class_name),
        f"{class_name}Properties.cs": generate_properties_class(class_name),
        f"{class_name}SetterHooks.cs": generate_setter_hooks_class(class_name),
    }

    for filename, content in files.items():
        file_path: Path = Path(f"{OUTPUT_DIR_PATH}/{filename}")
        if file_path.exists():
            print(f"Warning: '{file_path}' already exists. Skipping.", file=sys.stderr)
            continue

        try:
            file_path.write_text(content, encoding="utf-8")
            print(f"Created: {file_path}")
        except IOError as e:
            print(f"Error writing '{file_path}': {e}", file=sys.stderr)
            exit(1)


if __name__ == "__main__":
    parser: argparse.ArgumentParser = argparse.ArgumentParser(
        description="Generate C# component class files for Triangulum.",
        formatter_class=argparse.RawDescriptionHelpFormatter,
    )
    parser.add_argument(
        "class_name",
        type=str,
        help="Name of the component class to create",
    )
    args: argparse.Namespace = parser.parse_args()

    create_class_files(args.class_name)
