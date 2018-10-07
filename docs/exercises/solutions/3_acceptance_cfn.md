# Exercise 3: Dynamic Security Testing Solution

Configuring the CFN NAG scanner requires the following execute shell command in the build step:

```bash
cfn_nag_scan -i src/infrastructure -o json > ${WORKSPACE}/cfn_nag.json || true
```