cmake_minimum_required(VERSION 3.31)

set(PROJECT_NAME autochassis)
project(${PROJECT_NAME} VERSION 1.0.0)

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED true)

# moves binary output to build/bin
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/bin)

# versioning
file(READ "${CMAKE_SOURCE_DIR}/VERSION" VERSION_STRING)
string(STRIP "${VERSION_STRING}" VERSION_STRING)

add_compile_definitions(
    APP_VERSION="${VERSION_STRING}"
)
message("Version: ${VERSION_STRING}")

# packages
# include(FetchContent)
# FetchContent_Declare(
#     tomlplusplus
#     GIT_REPOSITORY https://github.com/marzer/tomlplusplus.git
#     GIT_TAG v3.4.0
# )
# FetchContent_MakeAvailable(tomlplusplus)

find_package(Qt6 REQUIRED COMPONENTS Core)

add_subdirectory(src)

get_property(all_targets DIRECTORY PROPERTY BUILDSYSTEM_TARGETS)
message(STATUS "Targets available: ${all_targets}")
