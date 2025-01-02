curl -X POST http://localhost:3000/addBody \
-H "Content-Type: application/json" \
-d '{"shape": "sphere", "mass": 5, "position": {"x": 0, "y": 10, "z": 0}}'

curl -X POST http://localhost:3000/applyForce \
-H "Content-Type: application/json" \
-d '{"bodyId": 0, "force": {"x": 0, "y": 0, "z": 10}, "point": {"x": 0, "y": 10, "z": 0}}'

curl -X POST http://localhost:3000/step \
-H "Content-Type: application/json" \
-d '{"dt": 0.016}'

curl -X GET http://localhost:3000/world \
-H "Content-Type: application/json"

curl -X POST http://localhost:3000/clear \
-H "Content-Type: application/json" 



// NEW

curl -X POST http://localhost:3000/api/stage -H "Content-Type: application/json" -d '{"id": "wildcat" }'

curl -X POST http://localhost:3000/api/mock -H "Content-Type: application/json" -d '{}'

curl -X GET http://localhost:3000/api/stream -H "Content-Type: application/json"

curl -X POST http://localhost:3000/api/addBody -H "Content-Type: application/json" -d '{"shapeType": "sphere", "mass": 5, "position": {"x": 0, "y": 10, "z": 0}}'
