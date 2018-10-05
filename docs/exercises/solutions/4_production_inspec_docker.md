# Exercise 4: Linux Hardening Guidelines Solution

Configuring the InSpec CIS Docker host scan requires the following command:

```bash
inspec exec https://github.com/dev-sec/cis-docker-benchmark --reporter json:cis.json html:cis.html junit:cis.xml || true
```