#!/usr/bin/env sh
set -e

./build.sh
./build/debug/bin/autochassis "$@"
