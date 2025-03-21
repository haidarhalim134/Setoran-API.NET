# Set the output file path
OUTPUT_FILE="./swagger.json"

# URL of the JSON to download
URL="http://localhost:5104/swagger/v1/swagger.json"

curl -o "$OUTPUT_FILE" "$URL"
openapi-generator-cli generate -i "$OUTPUT_FILE" -g dart -o "./apilib/dart"
rm "$OUTPUT_FILE"
rm "./openapitools.json"