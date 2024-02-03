#!/bin/sh
echo -ne '\033c\033]0;Project\a'
base_path="$(dirname "$(realpath "$0")")"
"$base_path/pre-build.x86_64" "$@"
