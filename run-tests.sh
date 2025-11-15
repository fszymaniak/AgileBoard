#!/bin/bash
set -e

echo "🧪 Running AgileBoard Tests..."
echo "================================"
echo ""

# Colors for output
GREEN='\033[0;32m'
RED='\033[0;31m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

# Check if dotnet is installed
if ! command -v dotnet &> /dev/null; then
    echo -e "${RED}❌ .NET SDK is not installed.${NC}"
    echo "Please install .NET 7.0 SDK from: https://dotnet.microsoft.com/download"
    exit 1
fi

# Check .NET version
echo -e "${YELLOW}📦 Checking .NET version...${NC}"
dotnet --version

# Restore dependencies
echo ""
echo -e "${YELLOW}📥 Restoring dependencies...${NC}"
dotnet restore

# Build the solution
echo ""
echo -e "${YELLOW}🔨 Building solution...${NC}"
dotnet build --no-restore --configuration Release

# Run the tests
echo ""
echo -e "${YELLOW}🧪 Running unit tests...${NC}"
dotnet test tests/AgileBoard.Tests.Unit/AgileBoard.Tests.Unit.csproj \
    --no-build \
    --configuration Release \
    --verbosity normal \
    --logger "console;verbosity=detailed"

# Check if tests passed
if [ $? -eq 0 ]; then
    echo ""
    echo -e "${GREEN}✅ All tests passed!${NC}"
    exit 0
else
    echo ""
    echo -e "${RED}❌ Tests failed!${NC}"
    exit 1
fi
