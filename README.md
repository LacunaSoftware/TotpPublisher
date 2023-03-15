# TotpPublisher

Source code for the docker image [lacunasoftware/totp-publisher](https://hub.docker.com/r/lacunasoftware/totp-publisher),
a minimalistic website to publish the current TOTP value for a given seed while keeping the seed hidden on the backend.

## Configuration

* `Totp__Seed`: the seed in Base32 **(required)**
* `Totp__Step`: the step in seconds (default: 30)
* `Totp__Mode`: the mode -- valid values: `Sha1` (default), `Sha256` and `Sha512`
* `Totp__Size`: the code size (default: 6)

## Example

docker run -e Totp__Seed=JBSWY3DPEHPK3PXP -p 80:80 lacunasoftware/totp-publisher
