# Local Setup with Kubernetes

This assumes that you are using Docker Desktop with Kubernetes enabled.

## Setup database

### Create namespace
`kubectl create namespace panorama`

### Switch to namespace context
`kubectl config set-context --current --namespace=panorama`

All commands will now apply to the `panorama` namespace.

### Apply network policy
This will create the network inside K8's through which your resources can communicate.

`kubectl apply -f .\kubernetes\networkpolicy.yaml`

### Create a stateful set for the database and expose on localhost
The stateful set will create the container resource for SQL Server, as well as a Persisted Claim and Volume to preserve the data.
A LoadBalancer service will also be created to make the database accessible via localhost.

`kubectl apply -f .\kubernetes\mssqldb-manifest.yaml`

At this point you can work locally against a database running in your Docker Desktop Kubernetes cluster.

## Setup Migrator 

It may or may not be useful to have a migrator running in the cluster (this is being listed so that developers working with Kubernetes and the boilerplate can practice and learn to understand the tools better.)

### Build the migrator Docker image
`docker build -t panorama.migrator -f .\src\Panorama.Migrator\Dockerfile .`

### Create a deployment to easily run the migrator through k8s
Note: if not scaled down, the migrator will continue to run periodically.
Use it / don't use it as you prefer.

`kubectl apply -f .\kubernetes\migrator-deployment.yaml`

### Scale down migrator to stop it running continuously
`kubectl scale deployment panorama-migrator --replicas=0`

### Scale up migrator to apply changes to the database
If you want to test coded changes, remember to re-build the image and apply the deployment first.

`kubectl scale deployment panorama-migrator --replicas=1`

At this point you can build a migrator image, deploy to the Kubernetes cluster, and scale the migrator to run it as it suits you.

This can be useful when starting from scratch with a clean database, and you just want to run the migrator code as is.

## Setup API

The web API needs to be hosted in order to service requests.

### Build the Host Docker image

NOTE: make sure to be aware of the ports that are being exposed in the Dockerfile.
Typically we expose both HTTP (80) and HTTPS (443) ports.

`docker build -t panorama.api -f .\src\Panorama.Web.Host\Dockerfile .`

### Create a deployment and service
Both deployment and service specs are included in a single manifest file. Apply the manifest to create the resources.

`kubectl apply -f .\kubernetes\api-manifest.yaml`

#### Use port forwarding to hit a pod without a service
This is useful for quick debugging, or if port mapping is being a pain.

`kubectl port-forward <pod>  <port>:<pod-port>`

This requires a terminal to keep the connection alive. Once closed, port forwarding will terminate.

## Setup Client App

The client app needs to be hosted to make the interface accessible via a browser.

### Build the Angular Docker image

NOTE: make sure to be aware of the ports that are being exposed in the Dockerfile.
Typically we expose both HTTP (80) and HTTPS (443) ports.

The app is using an NGINX load balancer to act as the web server.

Note: nginx is being used simply to serve the files (static file server), rather than a load balancer.
Traffic is managed by the Ingress.

`docker build -t panorama.app -f .\src\Panorama.Web.Host\Angular.Dockerfile .`

### Create a deployment and service
Both deployment and service specs are included in a single manifest file. Apply the manifest to create the resources.

`kubectl apply -f .\kubernetes\app-manifest.yaml`

## Setup the Causation API

The causation API is responsible for Causation sourcing and validation.

## Build the Docker image
`docker build -t causation.api -f .\src\Panorama.Causation\Dockerfile .`

## Deploy the deployment and service
`kubectl apply -f .\kubernetes\causation-manifest.yaml`

# Additional Services

## Rabbit MQ Service Bus
RabbitMQ is used to faciliate inter-service communication.

It is hosted using a Stateful Set, which provides stable network identities, ensuring that the pods get predictable hostnames and persistent storage, which is essential for RabbitMQ clusters.

While this example uses a single replica, you can increase the replicas count for horizontal scaling. Ensure you configure RabbitMQ clustering appropriately.

`kubectl apply -f .\kubernetes\rabbitmq-manifest.yaml`

AMQP API: http://localhost:5672/
Management API: http://localhost:15672/

The default username / password is guest / guest.

## MongoDB
MongoDB is used to persist data using NOSQL.

It is hosted using a Stateful Set, which provides stable network identities, ensuring that the pods get predictable hostnames and persistent storage, which is essential for RabbitMQ clusters.

While this example uses a single replica, you can increase the replicas count for horizontal scaling. Ensure you configure MongoDb clustering appropriately.

`kubectl apply -f .\kubernetes\mongodb-manifest.yaml`

Connection string: `mongodb://admin:admin@localhost:27017/`

The default username / password is admin / admin

All collections will be automatically provisioned by the driver Nuget package.

# Resolve a DNS
Update the following environment files with the DNS:

ABP Host API:
`src/Panorama.Web.Host/appsettings.<ENV>.json`
Angular Client APP (always production file):
`src/Panorama.Web.Host/src/assets/appconfig.production.json`

## Apply an Ingress Controller
`kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/master/deploy/static/provider/cloud/deploy.yaml`

## For Local Development
For Windows, go and update your host file:

Add a rule to route the DNS to localhost:
`127.0.0.1 panorama.local`

### DNS per Ingress
```
127.0.0.1     panorama.local
127.0.0.1     serve.panorama.local
127.0.0.1     scenography.panorama.local
```

# Secure the Application via HTTPS

## For Local Development
Make use of [mkcert](https://github.com/FiloSottile/mkcert) to manage certificates locally.

`mkcert -install`

Generate certificate as PEM files:
`mkcert panorama.local *.panorama.local`

Create a secret in the cluster that stores the TLS certificate:
`kubectl create secret tls tls-secret --cert=.\kubernetes\certs\panorama.local.pem --key=.\kubernetes\certs\panorama.local-key.pem`

Refer to the [documentation](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel/endpoints?view=aspnetcore-8.0) when setting up the certificate resolution to Kestrel:

```json
{
  "Kestrel": {
    "Endpoints": {
      "Http": {
        "Url": "http://localhost:5000"
      },
      "HttpsInlineCertFile": {
        "Url": "https://localhost:5001",
        "Certificate": {
          "Path": "<path to .pfx file>",
          "Password": "$CREDENTIAL_PLACEHOLDER$"
        }
      },
      "HttpsInlineCertAndKeyFile": {
        "Url": "https://localhost:5002",
        "Certificate": {
          "Path": "<path to .pem/.crt file>",
          "KeyPath": "<path to .key file>",
          "Password": "$CREDENTIAL_PLACEHOLDER$"
        }
      },
      "HttpsInlineCertStore": {
        "Url": "https://localhost:5003",
        "Certificate": {
          "Subject": "<subject; required>",
          "Store": "<certificate store; required>",
          "Location": "<location; defaults to CurrentUser>",
          "AllowInvalid": "<true or false; defaults to false>"
        }
      },
      "HttpsDefaultCert": {
        "Url": "https://localhost:5004"
      }
    },
    "Certificates": {
      "Default": {
        "Path": "<path to .pfx file>",
        "Password": "$CREDENTIAL_PLACEHOLDER$"
      }
    }
  }
}
```

It is also possible to generate a PFX file:
`mkcert -pkcs12 panorama.local *.panorama.local`

You can store the PFX as a generic secret, but this didn't seem to work:
`kubectl create secret generic tls-secret --from-file=panorama.local.pfx`

If you get stuck with 2 PEM files, and you need to convert them to PFX:
`openssl pkcs12 -export -out app.local.pfx -inkey app.local-key.pem -in app.local.pem -password pass:yourpassword`

Verify certificate contents:
`openssl x509 -in app.local.pem -text -noout`

## Switch project context
In order to switch contexts to a different project, `localhost` needs to be freed up. Any services that exist in the cluster are likely bound to localhost, and will block other applications from using it.

### Delete services to free up localhost
`kubectl delete svc --all`

The above command will delete all services in your current namespace, but will leave the other cluster resources intact. When you want to come back to this project, you can provision the services, and they will re-acttach to the existing resources.

While this can be useful for day-to-day context-switching, don't leave too many resources lying around in the cluster long-term, as they will consume hardware resources even if they are not in use.

If you are in a different namespace (eg. you switched and forgot to clear the services bound to localhost), you can do the following:

#### Declare the target namespace in the command
`kubectl delete svc --all --namespace=panorama` OR `kubectl delete svc --all -n=panorama`

# Bash into Pod to Debug
`kubectl exec --stdin --tty <pod_name> -- /bin/bash`

# Quickfire section

kubectl config set-context --current --namespace=panorama

kubectl apply -f .\panorama\kubernetes\networkpolicy.yaml



SEPARATE API AND APP INGRESS

~~kubectl apply -f .\kubernetes\ingress-manifest.yaml~~



PANORAMA API (http://localhost:44312/) ---

MSSQL
kubectl apply -f .\panorama\kubernetes\mssqldb-manifest.yaml

kubectl port-forward mssqldb-0 1433:1433

MIGRATOR
docker build -t panorama.migrator -f .\panorama\src\Panorama.Migrator\Dockerfile .
kubectl apply -f .\panorama\kubernetes\migrator-manifest.yaml

kubectl scale deployment panorama-migrator --replicas=0

~~docker build -t panorama.api -f .\src\Panorama.Web.Host\Dockerfile .~~
~~kubectl apply -f .\kubernetes\api-manifest.yaml~~



PANORAMA APP (http://localhost:4201/) ---

~~docker build -t panorama.app -f .\src\Panorama.Web.Host\Angular.Dockerfile .~~
~~kubectl apply -f .\kubernetes\app-manifest.yaml~~


RABBITMQ

kubectl apply -f .\panorama\kubernetes\rabbitmq-manifest.yaml

kubectl port-forward rabbitmq-0 15672:15672


COMBINED PANORAMA ---

APPI
docker build -t panorama.appi -f .\panorama\src\Panorama.Web.Host\Combined.Dockerfile .

kubectl apply -f .\panorama\kubernetes\appi-panorama-manifest.yaml


TEATRO API (http://localhost:8484/) ---

MONGODB
kubectl apply -f .\panorama\kubernetes\mongodb-manifest.yaml

kubectl port-forward mongodb-0 27017:27017

POSTGRESDB
kubectl apply -f .\panorama\kubernetes\postgresdb-manifest.yaml

kubectl port-forward postgresdb-0 5432:5432

docker build -t teatro.api -f .\teatro\Teatro\Teatro.Application\Dockerfile .

kubectl apply -f .\panorama\kubernetes\teatro-manifest.yaml


TLS SECRET

LOCAL (OBSOLETE)
kubectl create secret tls tls-secret --cert=.\panorama\kubernetes\certs\panorama.local+1.pem --key=.\panorama\kubernetes\certs\panorama.local+1-key.pem

STAGING
kubectl create secret tls tls-secret --cert=.\panorama\kubernetes\certs\panorama.staging+1.pem --key=.\panorama\kubernetes\certs\panorama.staging+1-key.pem


INGRESS

kubectl apply -f .\panorama\kubernetes\staging-ingress-manifest.yaml




--kubectl apply -f .\kubernetes\azurite-manifest.yaml             ????


DOMINO (http://localhost:7474)
docker build -t causation.api -f .\src\Panorama.Delta\Dockerfile .
kubectl apply -f .\kubernetes\causation-manifest.yaml


kubectl apply -f .\kubernetes\mongodb-manifest.yaml





QUICK START LOCAL BACKING:

MSSQL
kubectl apply -f .\panorama\kubernetes\mssqldb-manifest.yaml
kubectl port-forward mssqldb-0 1433:1433

MONGODB
kubectl apply -f .\panorama\kubernetes\mongodb-manifest.yaml
kubectl port-forward mongodb-0 27017:27017

POSTGRESDB
kubectl apply -f .\panorama\kubernetes\postgresdb-manifest.yaml
kubectl port-forward postgresdb-0 5432:5432

RABBITMQ
kubectl apply -f .\panorama\kubernetes\rabbitmq-manifest.yaml
kubectl port-forward rabbitmq-0 15672:15672


