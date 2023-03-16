# TOTP Publisher

Source code for the docker image [lacunasoftware/totp-publisher](https://hub.docker.com/r/lacunasoftware/totp-publisher),
a minimalistic website to publish the current TOTP value for a given seed while keeping the seed hidden on the backend.

## Configuration

* `Totp__Seed`: the seed in Base32 **(required)**
* `Totp__Step`: the step in seconds (default: 30)
* `Totp__Mode`: the mode -- valid values: `Sha1` (default), `Sha256` and `Sha512`
* `Totp__Size`: the code size (default: 6)
* `General__SiteName`: site name
* `General__Debug`: set to `True` to show the TOTP configuration on the website (make sure you change your seed afterwards)

To generate a random seed, simply run the image:

```sh
docker run lacunasoftware/totp-publisher
```

## Example

```sh
docker run -e Totp__Seed=3QXEF33LKVP4U5DIO534UBOGCM -p 80:80 lacunasoftware/totp-publisher
```

## Live demo

Check out a live demo [here](https://psc-lacuna-diagkey.azurewebsites.net/).

## Acknowledgements

This project uses [Otp.NET](https://github.com/kspearrin/Otp.NET) written by [Kyle Spearrin](https://github.com/kspearrin).
