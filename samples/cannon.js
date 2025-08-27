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

curl -X POST http://localhost:3000/api/stage -H "Content-Type: application/json" -d '{"id": "simulator" }'

curl -X POST http://localhost:3000/api/mock -H "Content-Type: application/json" -d '{}'

curl -X GET http://localhost:3000/api/stream -H "Content-Type: application/json"

shapeOptions

// BOXES
curl -X POST http://localhost:3000/api/addBody -H "Content-Type: application/json" -d '{"worldId": "simulator", "shapeType": "box", "mass": 3, "position": {"x": 1, "y": 0, "z": 0}, "shapeOptions": { "width": 5, "height": 5, "depth": 5 }}'
curl -X POST http://localhost:3000/api/addBody -H "Content-Type: application/json" -d '{"worldId": "simulator", "shapeType": "box", "mass": 2, "position": {"x": 3, "y": 2, "z": 1}, "shapeOptions": { "width": 2, "height": 6, "depth": 3 }}'
curl -X POST http://localhost:3000/api/addBody -H "Content-Type: application/json" -d '{"worldId": "simulator", "shapeType": "box", "mass": 1, "position": {"x": 5, "y": 4, "z": 2}, "shapeOptions": { "width": 2, "height": 2, "depth": 6 }}'


curl -X POST http://localhost:3000/api/addBody -H "Content-Type: application/json" -d '{"worldId": "simulator", "shapeType": "sphere", "mass": 5, "position": {"x": 0, "y": 5, "z": 0}, "shapeOptions": { "radius": 5 }}'