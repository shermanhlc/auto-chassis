set(SOURCES
    main.cpp
)

add_executable(${PROJECT_NAME}
    ${SOURCES}
)

add_subdirectory(utils)
add_subdirectory(chassis)

target_link_libraries(${PROJECT_NAME}

PRIVATE
    utils
    chassis

    Qt6::Core
)

target_include_directories(${PROJECT_NAME}
PRIVATE
    ${CMAKE_CURRENT_SOURCE_DIR}
)
