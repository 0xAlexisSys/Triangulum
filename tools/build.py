import os


SOURCE_DIR_PATH: str = "src"


if __name__ == "__main__":
    for dir_name in os.listdir(SOURCE_DIR_PATH):
        dir_path: str = f"{SOURCE_DIR_PATH}/{dir_name}"
        if os.path.isdir(dir_path):
            os.system(f"dotnet build \"{dir_path}\"")
