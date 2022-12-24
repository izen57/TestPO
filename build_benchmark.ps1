docker build -f Benchmark/Dockerfile .
cd Benchmark
docker run -v E:\TestPO\Benchmark\"Logs and results":/app/bin/publish/BenchmarkDotNet.Artifacts --privileged -it benchmark --filter * --memory --logBuildOutput
cd ..