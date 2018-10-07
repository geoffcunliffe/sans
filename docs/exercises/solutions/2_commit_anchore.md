# Exercise 2: Anchore Container Scanning

The following line of code will create the **anchore_images** file with the correct values and format:

```bash
echo "${REPOSITORY_URI}:${TAG} ${WORKSPACE}/src/app/Dockerfile " > anchore_images
```
