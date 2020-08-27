<?php

class Faculties {

    public $fakultetId;
    public $FacultyId;
    public $Name;

    private $conn;

    public function __construct($db){
        $this->conn=$db;
    }

    public function getAllFaculties(){

        $query="SELECT * FROM faculties";

        $stmt = $this->conn->prepare($query);
        // execute query
        $stmt->execute();
        $num = $stmt->rowCount();

        // check if more than 0 record found
        if($num>0){
            // products array
            $response=array();
            // retrieve our table contents
            while($row = $stmt->fetch(PDO::FETCH_OBJ)) {
                $response[]=$row;
            }
            // set response code - 200 OK
            http_response_code(200);
            return json_encode($response);
        }
        else{
            // set response code - 404 Not found
            http_response_code(404);

            // tell the user no products found
            return json_encode(
                array("message" => "Fakulteti nisu pronadjeni.")
            );
        }

        //return $stmt;
    }
    public function getFaculties($fakultetId){

      if (is_numeric($fakultetId)){
        $query="SELECT * FROM faculties where FacultyId=?";

        $stmt = $this->conn->prepare($query);
        $stmt->bindParam(1, $fakultetId);
        // execute query
        $stmt->execute();
        // get retrieved row
        $row = $stmt->fetch(PDO::FETCH_ASSOC);

        if($row!=null){

            // set response code - 200 OK
            http_response_code(200);

            return json_encode($row);

        }
        else{
            // set response code - 404 Not found
            http_response_code(404);

            // tell the user product does not exist
            return json_encode(array("message" => "Fakultet ne postoji."));
        }

      }else{
          // set response code - 404 Not found
          http_response_code(404);

          // tell the user product does not exist
          return json_encode(array("message" => "Fakultet ne postoj."));
          //return http_response_code();
      }
    }
    public function createFaculties(){
        //$data = json_decode(file_get_contents('php://input'), true);


        // query to insert record
        $query ="INSERT INTO `faculties`(`Name`)
                VALUES (?)";

        // prepare query
        $stmt = $this->conn->prepare($query);


            // bind values
            
            $stmt->bindParam(1, $this->Name);
          
            // execute query
            if ($stmt->execute()) {
            return true;
            }

            return false;

    }

    public function updateFaculties($fakultetId){
        // query to insert record
        $query ="UPDATE `faculties` SET `Name`=? WHERE FacultyId=?";

        // prepare query
        $stmt = $this->conn->prepare($query);


            // bind values
            $stmt->bindParam(1, $this->Name);
            $stmt->bindParam(2, $fakultetId);

            // execute query
            if ($stmt->execute()) {
            return true;
            }

            return false;
    }

    public function deleteFaculties($fakultetId){
        $query ="DELETE FROM `faculties` WHERE FacultyId=?";

        // prepare query
        $stmt = $this->conn->prepare($query);


            // bind values
            $stmt->bindParam(1, $fakultetId);

            // execute query
            if ($stmt->execute()) {
            return true;
            }
            return false;
    }

}


?>
