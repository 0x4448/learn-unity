# Repository Setup

Before starting to work on the project, the repository must be configured.

## gitignore
GitHub has a [gitignore file](https://github.com/github/gitignore/blob/main/Unity.gitignore)
for Unity projects.

## gitattributes
The [gitattributes file](https://git-scm.com/docs/gitattributes) is used to
configure file attributes. For example, it can be used to tell git about:

- CRLF for `.cs` files instead of LF like other files
- file types that use Unity's YAML merge tool
- setup [large file storage](https://git-lfs.github.com) for some file types

## YAML merge
The (UnityYAMLMerge)[https://docs.unity3d.com/Manual/SmartMerge.html] tool is
used to merge Unity's YAML files, which includes scenes and prefabs. This
should be set in `$GIT_DIR/config`, where `$GIT_DIR` is the repository's hidden
`.git` directory.

The [official tutorial](https://learn.unity.com/tutorial/working-with-yamlmerge)
suggests using `$HOME/.gitconfig`. This eliminates the need to configure on a
per-project basis, but there is no guarantee that the YAML merge tool from one
version of Unity is compatible with projects made with other versions.
